import { Component, Input, OnInit } from '@angular/core';
import { ProfileSearchResult } from 'src/app/models/search-result/profileSearchResult';

@Component({
  selector: 'app-search-result-item',
  templateUrl: './search-result-item.component.html',
  styleUrls: ['./search-result-item.component.css']
})
export class SearchResultItemComponent implements OnInit {

  constructor() { }

  @Input() Profile: ProfileSearchResult;

  ngOnInit(): void {
  }

}
