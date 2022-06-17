import { RegionService } from './../../../services/regions-service/region.service';
import { UserProfile } from './../../../models/UserProfile';
import { CustomValidatorsService } from './../../../services/custom-validators/custom-validators.service';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Regions } from '../../../enums/regions';
import { Genders } from 'src/app/enums/Genders';
import { UserService } from 'src/app/services/user-service/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
    public customValidatorsService: CustomValidatorsService,
    private userService: UserService, private regionService: RegionService)
  {}

  editProfileForm: FormGroup;
  regions: string[] = [];
  genders: string[] = [];
  private currentProfile: UserProfile | undefined;
  private profileImage : string;

  async ngOnInit(): Promise<void> {

    this.regions = this.regionService.GetAllRegions();
    this.genders = Object.keys(Genders).filter(f => isNaN(Number(f)));

    this.currentProfile = await this.userService.GetFullProfile().toPromise();

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
      ],
      photo: [
        this.currentProfile?.photo, [
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
  
  async OnFileUpload(event: Event)
  {
    const input = event.currentTarget as HTMLInputElement;
    const file = input.files?.item(0);
    
    if(file)
    {
      this.profileImage = await this.ConvertToBase64(file) as string;
    } 
  }

  private ConvertToBase64(file: File)
  {
    return new Promise((resolve, reject) =>
    {
      const reader = new FileReader();
      reader.readAsDataURL(file);

      reader.onload = () => resolve(reader.result);
      reader.onerror = (error) => reject(error);
    });
  }

  private ParseDate(date: string): string | undefined
  {
    return date.split("T").shift();
  }

  private ParsePhone(phone: string): string | undefined
  {
    return phone?.split('+380').pop()
  }

  private GetProfileData()
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
        photo : this.profileImage
      }

      return profile;
  }


  OnApply()
  {
    const userProfile = this.GetProfileData();
    console.log(userProfile);
    this.userService.ChangeProfile(userProfile).subscribe(
      () => window.location.reload()
    );
  }
}


