import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchResultPaginationComponent } from './search-result-pagination.component';

describe('SearchResultPaginationComponent', () => {
  let component: SearchResultPaginationComponent;
  let fixture: ComponentFixture<SearchResultPaginationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchResultPaginationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchResultPaginationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
