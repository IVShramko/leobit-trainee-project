import { AlbumFullDTO } from '../../../models/albumFullDTO';
import { AlbumCreateDTO } from '../../../models/albumCreateDTO';
import { CustomValidatorsService } from './../../../services/custom-validators/custom-validators.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlbumService } from './../../../services/album-service/album.service';
import { AlbumMainDTO } from '../../../models/albumMainDTO';
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

  albums: AlbumMainDTO[];

  albumForm: FormGroup;
  id: string = '';

  ngOnInit(): void {
    this.LoadPageData();

    this.albumForm = this.formBuilder.group({
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

    this.albumForm.controls.name
      .addAsyncValidators(
        this.customValidatorsService.AlbumNameValidator())
  }

  get name() {
    return this.albumForm.controls.name;
  }

  get description() {
    return this.albumForm.controls.description;
  }

  private LoadPageData() {
    this.albumService.GetAllAlbums().subscribe(
      (albums) => this.albums = albums
    )
  }

  OpenAlbum(album: AlbumMainDTO) {
    this.router.navigate([album.id], { relativeTo: this.route });
  }

  CreateAlbum() {
    const newAlbum: AlbumCreateDTO = {
      name: this.name?.value,
      description: this.description?.value
    }

    this.albumService.CreateAlbum(newAlbum).subscribe(
      (res) => {
        this.LoadPageData();
      }
    )
  }

  LoadFullAlbum(id: string) {
    this.albumService.GetAlbumById(id).subscribe(
      (album) => {
        this.id = album.id;
        this.name?.setValue(album.name);
        this.description?.setValue(album.description)
      }
    )
  }

  EditAlbum() {
    const newAlbum: AlbumFullDTO = {
      id: this.id,
      name: this.name?.value,
      description: this.description?.value
    }

    this.albumService.UpdateAlbum(newAlbum).subscribe(
      (res) => this.LoadPageData()
    )
  }

  DeleteAlbum() {
    this.albumService.DeleteAlbum(this.id).subscribe(
      (res) => this.LoadPageData(),
    )
  }
}
