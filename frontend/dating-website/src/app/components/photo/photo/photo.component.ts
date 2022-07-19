import { PhotoMain } from 'src/app/models/PhotoMain';
import { AfterContentInit, Input, Output, EventEmitter } from '@angular/core';
import { Component, ContentChild, ElementRef, OnInit, Renderer2, } from '@angular/core';

@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.css']
})
export class PhotoComponent implements OnInit, AfterContentInit {

  @ContentChild('image') image: ElementRef;
  @Input() photo: PhotoMain;

  @Output('onDelete') deleteId = new EventEmitter<string>();
  @Output('OnCarouselViewActivated') activeId = new EventEmitter<PhotoMain>();

  isFocused: boolean = false;

  constructor(private renderer: Renderer2) { }

  ngOnInit(): void {
  }

  ngAfterContentInit(): void {
    const img = this.image.nativeElement;
    this.renderer.addClass(img, 'photo')
  }

  OnToggleFocus(value: boolean)
  {
    this.isFocused = value;
  }

  OnDelete()
  {
    this.deleteId.emit(this.photo.id);
  }

  OnActivateCarouselView()
  {
    this.activeId.emit(this.photo);
  }
}
