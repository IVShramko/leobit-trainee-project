import { Component, Input, OnInit } from '@angular/core';
import { ProfileSearchResult } from 'src/app/models/search-result/profileSearchResult';

@Component({
  selector: 'app-search-result-page',
  templateUrl: './search-result-page.component.html',
  styleUrls: ['./search-result-page.component.css']
})
export class SearchResultPageComponent implements OnInit {

  constructor() { }

  @Input() UserProfiles: ProfileSearchResult[] | null;

  ngOnInit(): void {

  }

}
