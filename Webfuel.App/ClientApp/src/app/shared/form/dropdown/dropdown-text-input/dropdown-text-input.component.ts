import { ChangeDetectionStrategy, Component, forwardRef } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import { DropDownBase } from '../dropdown-base';

@Component({
    selector: 'dropdown-text-input',
    templateUrl: './dropdown-text-input.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => DropDownTextInputComponent),
            multi: true
        }
    ]
})
export class DropDownTextInputComponent<TItem> extends DropDownBase<TItem> implements ControlValueAccessor {

    ngOnInit(): void {
        super.ngOnInit();
        this.focusControl.valueChanges
            .pipe(
                debounceTime(200),
                tap(value => this.onFocusControlChange(value)),
                takeUntilDestroyed(this.destroyRef),
            )
            .subscribe();
    }

    onFocusControlChange(value: string | null) {
        this.onChange(value);
    }

    // Client Events

    pickItem(item: TItem) {
        if (this._isDisabled)
            return;

        this.closePopup();
        this.focusControl.setValue((<any>item).name);
        this.cd.detectChanges();
    }

    clear() {
        if (this._isDisabled)
            return;

        this.closePopup();
        this.focusControl.setValue('');
        this.cd.detectChanges();
    }

    doChangeCallback() {
    }

    // ControlValueAccessor API

    public writeValue(value: string | null): void {
        this.focusControl.setValue(value, { emitEvent: false });
    }

    public registerOnChange(fn: (value: string | null) => void): void {
        this.onChange = fn;
    }
    onChange: (value: string | null) => void = noop;
}
