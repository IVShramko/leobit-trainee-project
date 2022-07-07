import { AlbumFull } from './../../../models/AlbumFull';
import { AlbumCreate } from './../../../models/AlbumCreate';
import { CustomValidatorsService } from './../../../services/custom-validators/custom-validators.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlbumService } from './../../../services/album-service/album.service';
import { AlbumMain } from './../../../models/AlbumMain';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-albums',
  templateUrl: './albums.component.html',
  styleUrls: ['./albums.component.css']
})
export class AlbumsComponent implements OnInit {

  constructor(private albumService: AlbumService, 
    private router: Router,
    private route: ActivatedRoute, 
    private formBuilder: FormBuilder,
    private customValidatorsService: CustomValidatorsService) { }

  albums: AlbumMain[];

  editForm: FormGroup;
  createForm: FormGroup;

  ngOnInit(): void {
    this.LoadPageData();

    this.createForm = this.formBuilder.group({
      name: [
        null, [
          Validators.required,
          Validators.pattern('^[a-zA-Z0-9_.-]+')
        ]
      ],
      description: [
        null,
        Validators.required
      ]
    })

    this.createForm.controls.name
      .addAsyncValidators(
        this.customValidatorsService.AlbumNameValidator())

    this.editForm = this.formBuilder.group({
      name: [
        null,[
          Validators.required,
          Validators.pattern('^[a-zA-Z0-9_.-]+')
        ]
      ]
    })

    this.editForm.controls.name
      .addAsyncValidators(
        this.customValidatorsService.AlbumNameValidator())
  }

  get editName()
  {
    return this.editForm.controls.name;
  }

  get createName()
  {
    return this.createForm.controls.name;
  }

  get description()
  {
    return this.createForm.controls.description;
  }

  private LoadPageData()
  {
    this.albumService.GetAllAlbums().subscribe(
      (albums) => this.albums = albums
    )
  }

  OpenAlbum(album: AlbumMain)
  {
    this.router.navigate([album.id], {relativeTo: this.route});
  }

  CreateAlbum()
  {
    const newAlbum: AlbumCreate = {
      name: this.createName?.value,
      description: this.description?.value
    }

    this.albumService.CreateAlbum(newAlbum).subscribe(
      (res) => {
        this.LoadPageData();
      }
    )
  }

  EditAlbum()
  {

  }
}
