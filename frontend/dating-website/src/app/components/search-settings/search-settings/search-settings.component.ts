import { FormBuilder, FormGroup } from '@angular/forms';
import { Component, Input, OnInit } from '@angular/core';
import { Output } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Filters } from 'src/app/enums/Filters';

@Component({
  selector: 'app-search-settings',
  templateUrl: './search-settings.component.html',
  styleUrls: ['./search-settings.component.css']
})
export class SearchSettingsComponent implements OnInit {

  constructor(private formBuilder: FormBuilder) { }

  settingsForm: FormGroup;
  filters: Filters;
  filterParameter: string;
  pageSize: number;
  @Output() sizeChangeEvent = new EventEmitter<number>();
  @Output() filterChangeEvent = new EventEmitter<string>();

  ngOnInit(): void {
    this.settingsForm = this.formBuilder.group({
      size: [
        2
      ],
      filter: [
        null
      ]
    })
  }
  
  get size()
  {
    return this.settingsForm.controls.size;
  }

  get filter()
  {
    return this.settingsForm.controls.filter;
  }

  ChangeSize()
  {
    this.sizeChangeEvent.emit(this.size?.value);
  }

  ChangeFilter()
  {
    this.filterChangeEvent.emit(Filters[this.filter?.value]);
  }
}
