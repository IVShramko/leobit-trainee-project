import { UserProfile } from 'src/app/models/UserProfile';
import { SearchResultUserProfile } from './../../../models/SearchResult';
import { SearchResult } from 'src/app/models/SearchResult';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-search-result-page',
  templateUrl: './search-result-page.component.html',
  styleUrls: ['./search-result-page.component.css']
})
export class SearchResultPageComponent implements OnInit {

  constructor() { }

  @Input() UserProfiles: SearchResultUserProfile[] | null;

  ngOnInit(): void {

  }

}
