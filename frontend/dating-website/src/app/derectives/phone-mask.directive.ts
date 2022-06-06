import { NgControl } from '@angular/forms';
import { Directive, HostListener } from '@angular/core';

@Directive({
  selector: '[PhoneMask]'
})
export class PhoneMaskDirective {

  private isBackspacePressed: boolean = false;

  @HostListener('ngModelChange')
  private onModelChange()
  {
    const currentValue = this.GetInputValue();
    const newValue = this.OnInputChanges(currentValue, this.isBackspacePressed);
    this.SetInputValue(newValue);
  }

  @HostListener('keydown.backspace') 
  private keydownBackspace()
  {
    this.isBackspacePressed = true;
  }

  @HostListener('keyup.backspace')
  private keyupBackspace()
  {
    this.isBackspacePressed = false;
  }
  
  constructor(private model: NgControl) { }

  private OnInputChanges(value: string, backspace: boolean)
  {
    if(!backspace)
    {
      return this.ApplyMask(value);
    }
    return this.Backspace(value);
  }

  private GetInputValue()
  {
    return this.model.value;
  }

  private SetInputValue(value: string)
  {
    this.model.valueAccessor?.writeValue(value);
  }

  private Backspace(value: string) : string
  {
    return value.substring(0, value.length);
  }

  private ApplyMask(value: string) : string
  {
    if (value.length === 1) {
      return value.replace(/[0-9]{0,2}/, '($&');
    }
    if (value.length === 3) {
      return value.replace(/[(][0-9]{0,4}/, '$&)-');
    }
    if (value.length === 8) {
      return value.replace(/[(][0-9]{2}[)][-][0-9]{3}/, '$&-');
    }
    
    return value;
  }
}


