import { AuthService } from 'src/app/services/authService/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
      password : [
        null,
        Validators.required
      ]
    });
  }

  get login()
  {
    return this.registerForm.controls.login;
  }

  get password()
  {
    return this.registerForm.controls.password;
  }

  OnRegister(userName: string, password: string)
  {
      this.authService.Register(userName, password);

      this.authService.isRegistered.subscribe(
        (result) => {
          console.log(result);
          this.regResult = result;
          this.isSend = true;
        }
      )
  }

}
