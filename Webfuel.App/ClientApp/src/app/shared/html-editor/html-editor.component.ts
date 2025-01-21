import { ChangeDetectionStrategy, Component, DestroyRef, forwardRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { TinymceOptions } from 'ngx-tinymce';
import { debounceTime, noop, tap } from 'rxjs';
import { RawEditorOptions } from 'tinymce';
import { ConfigurationService } from '../../core/configuration.service';

@Component({
  selector: 'html-editor',
  templateUrl: './html-editor.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => HtmlEditorComponent),
      multi: true
    }
  ]
})
export class HtmlEditorComponent implements ControlValueAccessor, OnInit {

  formControl: FormControl = new FormControl<string>('');

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private configurationService: ConfigurationService
  ) {
  }

  ngOnInit(): void {
    this.formControl.valueChanges
      .pipe(
        debounceTime(200),
        tap(value => {
          if (value == this._value)
            return;
          this._value = value;
          this.onChange(value);
        }),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe();
  }
 
  _value: string = ""; // TinyMCE sends value change at load so need to avoid making the form dirty

  // Inputs

  @Input()
  placeholder = "";

  @Input()
  height: string | number | undefined = 450;

  public onBlur(): void {
    this.onTouched();
  }

  // TinyMCE configuration

  get config(): RawEditorOptions {

    var toolbar = 'undo redo | copy cut paste pastetext | bold italic underline strikethrough | bullist numlist | link openlink unlink | alignleft aligncenter alignright alignnone | fullscreen'
    if (this.configurationService.hasClaim(c => c.claims.developer))
      toolbar += ' | code';

    return {
      promotion: false,
      height: this.height,
      plugins: 'lists fullscreen link code wordcount',
      toolbar: toolbar,
      menubar: '',
      statusbar: true,
      highlight_on_focus: false,
      license_key: 'gpl',
      placeholder: this.placeholder,
      branding: false,
      formats: {
        underline: { inline: 'u', exact: true },
      },
      content_css: [
        "https://fonts.googleapis.com/css2?family=Poppins"
      ],
      content_style: `
        body {
          font-family: "Poppins", sans-serif;
          font-size: 16px;
        }
        p {
          margin: 16px 0 16px 0;
        }
      `
    }
  }

  // ControlValueAccessor API

  onChange: (value: string) => void = noop;
  onTouched: () => void = noop;

  public writeValue(value: string): void {
    this._value = value;
    this.formControl.setValue(value, { emitEvent: false });
    setTimeout(() => { 
      this.formControl.setValue(value, { emitEvent: false })
    }, 250)
  }

  public registerOnChange(fn: (value: string) => void): void {
    this.onChange = fn;
  }

  public registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  public setDisabledState?(isDisabled: boolean): void {
    isDisabled ? this.formControl.disable() : this.formControl.enable();
  }
}
