import { ChangeDetectionStrategy, Component, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { noop } from 'rxjs';
import _ from 'shared/common/underscore';
import { DropDownBase } from '../dropdown-base';

@Component({
    selector: 'dropdown-multi-select',
    templateUrl: './dropdown-multi-select.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => DropDownMultiSelectComponent),
            multi: true
        }
    ]
})
export class DropDownMultiSelectComponent<TItem> extends DropDownBase<TItem> implements ControlValueAccessor {

    @Input()
    verticalStack: boolean = false;

    @Input()
    closeOnSelect: boolean = false;

    get showInlineTags() {
        return this.verticalStack === false && this.pickedItems.length > 0;
    }

    // Client Events

    pickItemEvent(item: TItem, $event: MouseEvent) {
        this.pickItem(item);
        if (this.closeOnSelect == false) {
            $event.preventDefault();
            this.popupRef?.updatePosition();
        }
    }

    pickItem(item: TItem) {
        if (this._isDisabled)
            return;

        var id = this.getId(item);

        if (_.some(this.pickedItems, p => this.getId(p) === id))
            this.removePickedItem(id);
        else
            this.pickItems([id], false);

        if (this.closeOnSelect)
            this.closePopup();

        this.doChangeCallback();
        this.cd.detectChanges();
    }

    removeItemEvent(item: TItem, $event: MouseEvent) {
        this.removeItem(item);
        $event.preventDefault();
        $event.stopImmediatePropagation();
    }

    removeItem(item: any) {
        if (this._isDisabled)
            return;

        this.removePickedItem(this.getId(item));
        this.doChangeCallback();
    }

    doChangeCallback() {
        this.onChange(_.map(this.pickedItems, (p) => this.getId(p)));
        this.checkFreeText();
    }

    // ControlValueAccessor API

    onChange: (value: string[] | null) => void = noop;

    public writeValue(value: string[] | null): void {
        this.pickItems(!value ? [] : value, true);
    }

    public registerOnChange(fn: (value: string[] | null) => void): void {
        this.onChange = fn;
    }
}
