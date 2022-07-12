import { PhotoMain } from 'src/app/models/PhotoMain';
import { AfterContentInit, Input, OnChanges } from '@angular/core';
import { Component, ContentChild, ElementRef, OnInit, Renderer2, } from '@angular/core';

@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.css']
})
export class PhotoComponent implements OnInit, AfterContentInit {

  @ContentChild('image') image: ElementRef;
  @Input() photo: PhotoMain;

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

}
