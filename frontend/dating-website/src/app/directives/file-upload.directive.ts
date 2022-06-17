import { HostListener } from '@angular/core';
import { Directive } from '@angular/core';

@Directive({
  selector: '[appFileUpload]'
})
export class FileUploadDirective {
  
  @HostListener('change') OnModelChange() 
  {
    console.log('ygvgv');
  }

  constructor() { }
}
