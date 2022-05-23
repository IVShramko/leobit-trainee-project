import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  constructor(private formBuilder: FormBuilder) { }

  editProfileForm: FormGroup;

  ngOnInit(): void {
    this.editProfileForm = this.formBuilder.group({
      userName : new FormControl({
        value : null,
        disabled : true
      },[
        Validators.required
      ]),
      firstName : [
        null,
        Validators.required
      ],
      lastName : [
        null,
        Validators.required
      ],
      email : [
        null, [
          Validators.required,
          Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')
        ]
      ],
      birthDate : [
        null,
        Validators.required
      ],
      gender : [
        null,
        Validators.required
      ]
    });
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

  get birthDate()
  {
    return this.editProfileForm.controls.birthDate;
  }


  private GetProfileData()
  {
    if (this.editProfileForm?.valid)
    {
      return {
        firstName : this.firstName?.value,
        lastName : this.lastName?.value
      }
    }
    return null;
  }

  OnApply()
  {
    const data = this.GetProfileData();
    console.log(data);
  }
}
