import { UserProfile } from './../../../models/UserProfile';
import { CustomValidatorsService } from './../../../services/custom-validators/custom-validators.service';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Regions } from '../../../enums/regions';
import { Genders } from 'src/app/enums/Genders';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
    public customValidatorsService: CustomValidatorsService) { }

  editProfileForm: FormGroup;
  regions: string[] = [];
  genders: string[] = [];

  ngOnInit(): void {
    this.regions = Object.keys(Regions).filter(f => isNaN(Number(f)));
    this.genders = Object.keys(Genders).filter(f => isNaN(Number(f)));

    this.editProfileForm = this.formBuilder.group({
      userName : new FormControl({
        value : null,
        disabled : true
      },[
        Validators.required
      ]),
      firstName : [
        null, [
          Validators.pattern('^[a-z]+'),
          Validators.required
        ]
      ],
      lastName : [
        null, [
          Validators.pattern('^[a-z]+'),
          Validators.required
        ]
      ],
      email : [
        null, [
          Validators.required,
          Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')
        ]
      ],
      phone : [
        null, [
          Validators.required,
          Validators.pattern('^[(][0-9]{2}[)][-][0-9]{3}[-][0-9]{4}$')
        ]
      ],
      birthDate : [
        null, [
          Validators.required,
          this.customValidatorsService.DateOfBirthValidator
        ]
      ],
      gender : [
        null,
        Validators.required,
      ],
      region : [
        null,
        Validators.required
      ],
      town : [
        null, [
          Validators.pattern('^[a-z]+'),
          Validators.required
        ]
      ],
      photo : [
        null, [
          Validators.required,
          this.customValidatorsService.FileTypeValidator
        ]
      ]
    });
  }

  get userName()
  {
    return this.editProfileForm.controls.userName;
  }

  get firstName()
  {
    return this.editProfileForm.controls.firstName;
  }

  get lastName()
  {
    return this.editProfileForm.controls.lastName;
  }

  get email()
  {
    return this.editProfileForm.controls.email;
  }

  get phone()
  {
    return this.editProfileForm.controls.phone;
  }

  get birthDate()
  {
    return this.editProfileForm.controls.birthDate;
  }

  get gender()
  {
    return this.editProfileForm.controls.gender;
  }

  get region()
  {
    return this.editProfileForm.controls.region;
  }

  get town()
  {
    return this.editProfileForm.controls.town;
  }

  get photo()
  {
    return this.editProfileForm.controls.photo;
  }

  private GetProfileData()
  {
    if (this.editProfileForm?.valid)
    {
      const profile: UserProfile = {
        userName : this.userName?.value,
        firstName : this.firstName?.value,
        lastName : this.lastName?.value,
        birthDate : this.birthDate?.value,
        gender : this.gender?.value,
        email : this.email?.value,
        phone : "+380" + this.phone?.value,
        region : this.region?.value,
        town : this.town?.value,
        photo : this?.photo?.value.split('\\').pop()
      }
      return profile;
    }
    return null;
  }

  OnApply()
  {
    const userProfile = this.GetProfileData();
    console.log(userProfile);
  }
}
