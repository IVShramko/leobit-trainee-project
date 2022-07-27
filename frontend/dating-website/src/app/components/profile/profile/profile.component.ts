import { PhotoMain } from 'src/app/models/PhotoMain';
import { PhotoService } from './../../../services/photo-service/photo.service';
import { RegionService } from './../../../services/regions-service/region.service';
import { UserProfile } from './../../../models/UserProfile';
import { CustomValidatorsService } from './../../../services/custom-validators/custom-validators.service';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Genders } from 'src/app/enums/Genders';
import { UserService } from 'src/app/services/user-service/user.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
    public customValidatorsService: CustomValidatorsService,
    private userService: UserService, 
    private photoService: PhotoService,
    private regionService: RegionService, 
    private sanitizer: DomSanitizer)
    {}
    
  editProfileForm: FormGroup;

  regions: string[] = [];
  genders: string[] = [];

  private currentProfile: UserProfile | undefined;

  profileImage: PhotoMain;

  ImageSourse: SafeResourceUrl;

  ngOnInit() {

    this.regions = this.regionService.GetAllRegions();
    this.genders = Object.keys(Genders).filter(f => isNaN(Number(f)));

    this.userService.GetFullProfile().subscribe(
      (result) => {

        this.currentProfile = result;

        this.photoService.GetPhotoById(this.currentProfile?.avatar as string)
            .subscribe(
                (result) => {
                  this.profileImage = result;
                  this.ImageSourse = 
                      this.ConvertToImage(result.data, result.name);
                });
        this.LoadForm();
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

  private LoadForm()
  {
    this.editProfileForm = this.formBuilder.group({
      userName: new FormControl({
        value: this.currentProfile?.userName,
        disabled: true
      }, [
        Validators.required
      ]),
      firstName: [
        this.currentProfile?.firstName, [
          Validators.pattern('^[a-z, A-Z]+'),
          Validators.required
        ]
      ],
      lastName: [
        this.currentProfile?.lastName, [
          Validators.pattern('^[a-z, A-Z]+'),
          Validators.required
        ]
      ],
      email: [
        this.currentProfile?.email, [
          Validators.required,
          Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')
        ]
      ],
      phone: [
        this.ParsePhone(this.currentProfile?.phoneNumber as string), [
          Validators.required,
          Validators.pattern('^[(][0-9]{2}[)][-][0-9]{3}[-][0-9]{4}$')
        ]
      ],
      birthDate: [
        this.ParseDate(this.currentProfile?.birthDate as string), [
          Validators.required,
          this.customValidatorsService.DateOfBirthValidator
        ]
      ],
      gender: [
        Genders[Number(this.currentProfile?.gender)],
        Validators.required,
      ],
      region: [
        this.currentProfile?.region,
        Validators.required
      ],
      town: [
        this.currentProfile?.town, [
          Validators.pattern('^[a-z, A-Z]+'),
          Validators.required
        ]
      ]
    });
  }

  ConvertToImage(base64: string, name: string): SafeResourceUrl
  {
    const extension = name.split('.').shift();
    
    return this.sanitizer.bypassSecurityTrustResourceUrl(
      `data:image/${extension};base64,` + base64 as string);
  }

  private ParseDate(date: string): string | undefined
  {
    return date.split("T").shift();
  }

  private ParsePhone(phone: string): string | undefined
  {
    return phone?.split('+380').pop()
  }

  private PrepareProfile()
  {
    const profile: UserProfile = {
      id : this.currentProfile?.id,
      userName : this.userName?.value,
      firstName : this.firstName?.value,
      lastName : this.lastName?.value,
      birthDate : this.birthDate?.value,
      gender : Boolean(Number(Genders[this.gender?.value as Genders])),
      email : this.email?.value,
      phoneNumber : "+380" + this.phone?.value,
      region : this.region?.value,
      town : this.town?.value,
      avatar : this.currentProfile?.avatar
    }

    return profile;
  }

  OnApply()
  {
    const userProfile = this.PrepareProfile();

    this.userService.ChangeProfile(userProfile).subscribe(
      () => window.location.reload()
    );
  }
}


