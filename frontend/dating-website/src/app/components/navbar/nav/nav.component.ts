import { takeLast, Subject, Observable, BehaviorSubject } from 'rxjs';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user-service/user.service';
import { MainData } from 'src/app/models/MainData';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public authService: AuthService, private router: Router,
    private userService: UserService) {}

  ngOnChanges()
  {
    this.LoadMainProfile();
    this.IsAuthenticated = this.authService.Authenticate();
  }

  ngOnInit(): void {}

  MainProfile = new Observable<MainData>();
  @Input() IsAuthenticated: boolean | null;

  private LoadMainProfile()
  {
    this.MainProfile = this.userService.GetMainProfile();
  }

  LogOut()
  {
    this.authService.LogOut().subscribe(
      (result) => {
        this.IsAuthenticated = false;
        result ? this.router.navigate(['/unauthorized']) : result
      }
    )
  }

}
