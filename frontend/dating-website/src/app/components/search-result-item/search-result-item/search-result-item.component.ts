import { SearchResult, SearchResultUserProfile } from 'src/app/models/SearchResult';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-search-result-item',
  templateUrl: './search-result-item.component.html',
  styleUrls: ['./search-result-item.component.css']
})
export class SearchResultItemComponent implements OnInit {

  constructor() { }

  @Input() Profile: SearchResultUserProfile;

  ngOnInit(): void {
  }
  
}
