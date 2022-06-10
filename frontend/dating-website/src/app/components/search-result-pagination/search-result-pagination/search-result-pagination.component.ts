import { Output } from '@angular/core';
import { Component, Input, OnInit } from '@angular/core';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-search-result-pagination',
  templateUrl: './search-result-pagination.component.html',
  styleUrls: ['./search-result-pagination.component.css']
})
export class SearchResultPaginationComponent implements OnInit {

  constructor() { }

  @Input() SearchResultTotal: number | null;
  @Input() pageSize : number;
  @Input() pageIndex : number;
  pageNumber: number;
  @Output() PageChangeEvent = new EventEmitter<number>();

  ngOnChanges(): void
  {
    this.pageNumber = this.CountPageNumber();
  }

  ngOnInit(): void {
  }

  private CountPageNumber()
  {
    return Math.ceil((this.SearchResultTotal ?? 0) / this.pageSize);
  }

  ChangePage(index: number)
  {
    this.PageChangeEvent.emit(index);
  }

}
