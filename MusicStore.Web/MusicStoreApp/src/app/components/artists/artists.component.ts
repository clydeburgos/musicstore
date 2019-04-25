import { Component, OnInit } from '@angular/core';
import { ArtistService } from 'src/app/services/artist.service';

import { Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, switchMap } from 'rxjs/operators';
@Component({
  selector: 'app-artists',
  templateUrl: './artists.component.html',
  styleUrls: ['./artists.component.scss']
})
export class ArtistsComponent implements OnInit {
  artists: any[] = [];
  artistNames: string[] = [];
  page = 1;
  pageSize = 10;
  searchInput: string = '';
  constructor(private artistService: ArtistService) { }

  ngOnInit() {
    this.getArtists();
  }

  getArtists(){
    this.artistService.getAllArtists().subscribe((res : any[]) => {
      this.artists = res;
    });
  }

  selectArtist(artist){

  }

  prev(){
    if(this.pageSize >= 10) {
      this.pageSize -= 10;
    }
    else {
      return false;
    }
    this.artistService.getArtistsPrev(this.pageSize).subscribe((res : any[]) => {
      this.artists = res;
    });
  }

  next(){
    this.pageSize += 10;
    this.artistService.getArtistsNext(this.pageSize).subscribe((res : any[]) => {
      this.artists = res;
    });
  }

  search = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      switchMap(term => {
        if(term.length < 2) return [];
        else {
          return this.artistService.getArtistNames(term);
        }
      })
    )
    formatter = (x: { Name: string }) => x.Name;

}
