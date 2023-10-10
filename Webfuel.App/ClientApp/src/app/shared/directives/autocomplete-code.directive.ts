import _ from '../underscore';
import { Directive, ElementRef, HostListener, OnInit, OnDestroy } from "@angular/core";
import { NgControl, FormGroup, FormControl, AbstractControl } from "@angular/forms";
import { pairwise } from 'rxjs/operators';

@Directive({
  selector: '[autocomplete-code]'
})
export class AutocompleteCodeDirective implements OnInit, OnDestroy {

  constructor(
    private el: ElementRef,
    private control: NgControl
  ) {
  }

  prev: string = ""; // #STOP# indicates we have stopped autocompletion

  name!: AbstractControl;
  code!: AbstractControl;

  ngOnInit(): void {

    if (!this.control || !this.control.control)
      return;

    var formGroup = <FormGroup>this.control.control.parent;
    if (!(formGroup instanceof FormGroup))
      return;

    this.name = formGroup.get('name')!;
    this.code = this.control.control!;

    if (!this.name || !this.code)
      return;

    this.prev = this.name.value || "";
    this.name.valueChanges.subscribe((next: string) => {
      if (this.prev == "#STOP#")
        return;
      var code = this.code.value || "";
      if (_.nameToCode(this.prev) == code) {
        this.code.setValue(_.nameToCode(next));
        this.prev = next || "";
      } else {
        this.prev = "#STOP#";
      }
    });
  }

  ngOnDestroy(): void {
  }
}
