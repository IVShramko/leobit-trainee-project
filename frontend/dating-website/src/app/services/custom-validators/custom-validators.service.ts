import { PhotoMain } from 'src/app/models/PhotoMain';
import { PhotoService } from './../photo-service/photo.service';
import { AlbumService } from 'src/app/services/album-service/album.service';
import { AbstractControl, AsyncValidatorFn, FormControl } from '@angular/forms';
import { Injectable } from '@angular/core';
import { map } from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class CustomValidatorsService{

  constructor(private albumService: AlbumService,
    private photoService: PhotoService) { }

  ConfirmPasswordValidator(control: AbstractControl)
  {
    if (control)
    {
      const password = control.root.get('password');
      const confpassword = control.root.get('confirmPassword');

      if (password && confpassword) {

        if (password?.value !== confpassword?.value) 
        {
          return {password_error : 'passwords do not match'}
        }
      }
    }
    return null;
  }

  DateOfBirthValidator(control: AbstractControl)
  {
    if(control)
    {
      if(control.value !== null)
      {
        const currentDate = Date.now();
        const birthDate = new Date(control?.value).getTime();
  
        let difference = Math.floor((currentDate - birthDate)/1000);
        difference = Math.floor(difference/3600);
        difference = Math.floor(difference/24);
        difference = Math.floor(difference/365);
  
        if (difference < 18) 
        {
          return {age_error : 'age can not be under 18'}
        }
      }
    }
    return null;
  }

  FileTypeValidator(control: FormControl)
  {
    if (control && control.value)
    {
      const allowedExtensions = ['jpg', 'jpeg', 'png'];
      const extension = control.value.split('.').pop().toLowerCase();

      if (!allowedExtensions.includes(extension))
      {
        return {file_type_error : 'file must be a image'}
      }
    }
    return null
  }

  AlbumNameValidator() : AsyncValidatorFn
  {
    return (control) => this.albumService.CheckNameValidity(control?.value)
      .pipe(
        map((res) => res ? null : {name_error: 'incorrect name'}))
  }

  PhotoNameValidator(photo: PhotoMain) : AsyncValidatorFn
  {
    const validator = (control: AbstractControl) => {
      const newName = control?.value;
      const extension = photo?.name.split('.').pop();

      const fullName = [newName, extension].join('.');

      return this.photoService.CheckNameValidity(photo.albumId, fullName)
      .pipe(
        map((res) => res ? null : {name_error: 'incorrect name'}))

    };
    return validator;
  }
}


