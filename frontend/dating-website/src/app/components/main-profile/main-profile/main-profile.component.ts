import { Component, Input, OnInit } from '@angular/core';
import { ProfileMainDTO } from 'src/app/models/profileMainDTO';

@Component({
  selector: 'app-main-profile',
  templateUrl: './main-profile.component.html',
  styleUrls: ['./main-profile.component.css']
})
export class MainProfileComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  @Input() MainProfile: ProfileMainDTO | null

}
