import { Component, Input, OnInit } from '@angular/core';
import { MainData } from 'src/app/models/MainData';

@Component({
  selector: 'app-main-profile',
  templateUrl: './main-profile.component.html',
  styleUrls: ['./main-profile.component.css']
})
export class MainProfileComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  @Input() MainProfile: MainData | null

}
