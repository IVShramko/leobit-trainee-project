import { UserService } from 'src/app/services/user-service/user.service';
import { PhotoService } from './../../../services/photo-service/photo.service';
import { CustomValidatorsService } from 'src/app/services/custom-validators/custom-validators.service';
import { PhotoMain } from 'src/app/models/PhotoMain';
import { AfterContentInit, Input, Output, EventEmitter } from '@angular/core';
import { Component, ContentChild, ElementRef, OnInit, Renderer2, } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.css']
})
export class PhotoComponent implements OnInit, AfterContentInit {

  @ContentChild('image') image: ElementRef;
  @Input() photo: PhotoMain;

  @Output('onDelete') deleteId = new EventEmitter<string>();
  @Output('OnCarouselViewActivated') active = new EventEmitter<PhotoMain>();

  isFocused: boolean = false;

  photoNameInput: FormControl;

  constructor(
    private formBuilder: FormBuilder,
    private renderer: Renderer2, 
    private photoService: PhotoService,
    private customValidatorsService: CustomValidatorsService,
    private userService: UserService) { }

  ngOnInit(): void {
    this.photoNameInput = this.formBuilder.control(
      null, {
      validators: Validators.required
    })

    this.photoNameInput.addAsyncValidators(
      this.customValidatorsService.PhotoNameValidator(this.photo))
  }

  ngAfterContentInit(): void {
    const img = this.image.nativeElement;
    this.renderer.addClass(img, 'photo')
  }

  get photoNameControl()
  {
    return this.photoNameInput;
  }

  SplitName()
  {
    let noExtensionName = this.photo.name.split('.');
    noExtensionName.pop();
    this.photoNameInput.setValue(noExtensionName);
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
    this.active.emit(this.photo);
  }

  OnRename()
  {
    const newPhoto = this.photo;
    const extension = this.photo.name.split('.').pop();

    newPhoto.name = [this.photoNameInput.value, extension].join('.');

    this.photoService.UpdatePhoto(newPhoto).subscribe(
      () => this.photoNameInput.reset()
    )
  }

  OnSetAsAvatar(id: string)
  {
    this.userService.SetProfileAvatar(id).subscribe();
  }
}
