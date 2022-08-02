import { Directive, HostListener, Output, EventEmitter } from '@angular/core';

@Directive({
  selector: '[appViewPhotoName]'
})
export class ViewPhotoNameDirective {

  constructor() { }

  @Output('imageHover') hover = new EventEmitter<boolean>();

  @HostListener('mouseenter') OnMouseEnter() {
    this.hover.emit(true);
  }

  @HostListener('mouseleave') OnMouseLeave() {
    this.hover.emit(false);
  }
}
