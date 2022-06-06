import { takeLast } from 'rxjs';
import { AuthService } from 'src/app/services/authService/auth.service';
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
    if (this.IsAuthenticated)
    {
      this.LoadMainProfile();
    }
  }

  ngOnInit(): void {
  }

  MainProfile: MainData | undefined

  @Input() IsAuthenticated: boolean | null;

  private async LoadMainProfile()
  {
    this.MainProfile = await this.userService.GetMainProfile().pipe(takeLast(1)).toPromise();
  }

  OnLogOut()
  {
    this.authService.LogOut(
      () => this.router.navigate(['/']));
  }

}
