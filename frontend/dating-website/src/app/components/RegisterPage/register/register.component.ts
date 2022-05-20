import { AuthService } from 'src/app/services/authService/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm : FormGroup;
  regResult: boolean;
  isSend: boolean;

  constructor(private formBuiler : FormBuilder,
    public authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.registerForm = this.formBuiler.group({
      login : [
        null,
        Validators.required
      ],
      email : [
        null, [
          Validators.required,
          Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')
        ]
      ],
      dateOfBirth : [
        null, [
          Validators.required,
          this.DateOfBirthValidator
        ]
      ],
      gender : [
        null,
        Validators.required
      ],
      password : [
        null,
        Validators.required
      ],
      confirmPassword : [
        null,[
          Validators.required,
          this.ConfirmPasswordValidator
        ]
      ]
    });
  }

  get login()
  {
    return this.registerForm.controls.login;
  }

  get email()
  {
    return this.registerForm.controls.email;
  }

  get dateOfBirth()
  {
    return this.registerForm.controls.dateOfBirth;
  }

  get gender()
  {
    return this.registerForm.controls.gender;
  }

  get password()
  {
    return this.registerForm.controls.password;
  }

  get confirmPassword()
  {
    return this.registerForm.controls.confirmPassword;
  }

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

  private GetUserData()
  {
    if(this.registerForm?.valid)
    {
      return {
        username : this.login?.value,
        email : this.email?.value,
        birthDate : this.dateOfBirth?.value,
        gender : this.gender?.value
      }
    }
    return null;
  }

  OnRegister()
  {
    const data = this.GetUserData();
    const login = this.login?.value;
    const password = this.password?.value;

    this.authService.Register(login, password);
    this.authService.isRegistered.subscribe(
      (result) => {
        this.regResult = result;
        this.isSend = true;
      })
  }
}
