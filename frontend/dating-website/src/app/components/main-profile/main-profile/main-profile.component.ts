import { UserService } from 'src/app/services/user-service/user.service';
import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { ProfileMainDTO } from 'src/app/models/profileMainDTO';

@Component({
  selector: 'app-main-profile',
  templateUrl: './main-profile.component.html',
  styleUrls: ['./main-profile.component.css']
})
export class MainProfileComponent implements OnInit, OnChanges {

  @Input() MainProfile: ProfileMainDTO | null

  constructor(private userService: UserService) { }

  ngOnChanges(): void {
    this.userService.currentAvatar.next(this.MainProfile?.avatar as string)
  }

  ngOnInit(): void {
  }
  
}
