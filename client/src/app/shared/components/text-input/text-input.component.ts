import { Component, OnInit, ViewChild, ElementRef, Input, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})

// ControlValueAccessor forma girilen verilerle işlem yapmak için kullanılır
export class TextInputComponent implements OnInit, ControlValueAccessor {
  // @ViewChild API'deki forma girilen inputlara ulaşmak için kullanılır.
@ViewChild('input', {static: true}) input: ElementRef;
@Input() type = 'text';
@Input() label: string;

  constructor(@Self() public controlDir: NgControl) {
    // contrpolDir'e bu component ve html içinde ulaşabiliriz böylece
    this.controlDir.valueAccessor = this;
  }

  ngOnInit() {
    const control = this.controlDir.control;
    const validators =  control.validator ? [control.validator] : [];
    const asyncValidators = control.asyncValidator ? [control.asyncValidator] : [];

    control.setValidators(validators);
    control.setAsyncValidators(asyncValidators);
    control.updateValueAndValidity();
  }

  onChange(event) {}

  onTouched() {}

  writeValue(obj: any): void {
    this.input.nativeElement.value = obj || '';
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
   this.onTouched = fn;
  }

}
