import { SearchService } from './../../../services/search-service/search.service';
import { Component, OnInit } from '@angular/core';
import { SearchResult } from 'src/app/models/SearchResult';
import { Observable, tap } from 'rxjs';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-result.component.html',
  styleUrls: ['./search-result.component.css']
})
export class SearchResultComponent implements OnInit {

  constructor(private searchService: SearchService) { }

  searchResults$ = new Observable<SearchResult[]>();

  ngOnInit(): void {
    this.searchService.criteria.subscribe(
      (criteria) => {
        console.log('2');
        this.searchResults$ = this.searchService.Search(criteria);
        console.log(this.searchResults$)
      }
    )
  }

}
