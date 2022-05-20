import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {

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
      email : new FormControl({
        value : null,
        disabled : true
      },[
        Validators.required
      ]),
      birthDate : new FormControl({
        value : null,
        disabled : true
      },[
        Validators.required
      ]),
      gender : new FormControl({
        value : null,
        disabled : true
      },[
        Validators.required
      ])
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
