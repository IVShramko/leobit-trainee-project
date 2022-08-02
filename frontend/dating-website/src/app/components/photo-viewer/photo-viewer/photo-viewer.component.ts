import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { PhotoMainDTO } from 'src/app/models/photoMainDTO';

@Component({
  selector: 'app-photo-viewer',
  templateUrl: './photo-viewer.component.html',
  styleUrls: ['./photo-viewer.component.css']
})
export class PhotoViewerComponent implements OnInit, OnChanges {

  @Input() photos: PhotoMainDTO[];
  @Input() focusedPhoto: PhotoMainDTO;

  focusedPhotoIndex: number;
  prewiewRowLength: number = 7;

  constructor(private sanitizer: DomSanitizer) { }

  ngOnChanges(changes: SimpleChanges): void {

    if (changes.photos && changes.focusedPhoto) {
      this.focusedPhotoIndex = this.photos.indexOf(this.focusedPhoto);
    }
  }

  ngOnInit(): void {
  }

  Next() {
    if (this.focusedPhotoIndex < this.photos.length - 1) {
      ++this.focusedPhotoIndex;
      this.focusedPhoto = this.photos[this.focusedPhotoIndex];
    }
  }

  Previous() {
    if (this.focusedPhotoIndex > 0) {
      --this.focusedPhotoIndex;
      this.focusedPhoto = this.photos[this.focusedPhotoIndex];
    }
  }
  OnChangeFocusImage(index: number) {
    this.focusedPhotoIndex = index;
    this.focusedPhoto = this.photos[index];
  }

  IsHidden(index: number): boolean {
    const a = Math.floor(this.prewiewRowLength / 2);

    let startIndex = this.focusedPhotoIndex - a;

    let lastIndex = this.focusedPhotoIndex + a;

    if (startIndex <= 0) {
      lastIndex += 0 - startIndex;
      startIndex = 0;
    }

    if (lastIndex >= this.photos.length) {
      startIndex -= lastIndex - this.photos.length + 1;
      lastIndex = this.photos.length;
    }

    if (index < startIndex || index > lastIndex) {
      return true;
    }

    return false;
  }

  ConvertToImage(base64: string, name: string): SafeResourceUrl {
    const extension = name.split('.').shift();

    return this.sanitizer.bypassSecurityTrustResourceUrl(
      `data:image/${extension};base64,` + base64 as string);
  }

}
