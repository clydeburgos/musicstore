import { Component, OnInit } from '@angular/core';
import { ArtistService } from 'src/app/services/artist.service';
import { ArtistModel } from 'src/app/models/artist.model';

@Component({
  selector: 'app-artist-detail',
  templateUrl: './artist-detail.component.html',
  styleUrls: ['./artist-detail.component.scss', '../artists/artists.component.scss']
})
export class ArtistDetailComponent implements OnInit {
  artistModel: ArtistModel;
  artistPhoto: string = '/assets/images/compact-disc 512.png';
  constructor(private artistService: ArtistService) { }

  ngOnInit() {
    this.artistModel = new ArtistModel();
  }

  getArtist(id: string){
    this.artistService.get(id).subscribe((res : any[]) => {
      if(res) {
        this.artistModel = res[0];
        this.getArtistPhoto(this.artistModel.Name);
      }
    })
  }

  getArtistPhoto(name: string){
    this.artistService.getImage(name).subscribe((res : any) => {
      this.artistPhoto = res.Url;
    });
  }
}
