import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ArtistService {
  baseAPIRoot: string = environment.baseUrl;
  baseAPIRoute: string = 'api/artists';
  baseAPIUrl: string = this.baseAPIRoot + this.baseAPIRoute;

  constructor(private http: HttpClient) { }

  getAllArtists(){
    return this.http.get(`${this.baseAPIUrl}?$top=10&$orderby=Name`);
  }

  get(id){
    return this.http.get(`${this.baseAPIUrl}?$filter=ArtistId eq ${id}`);
  }

  getImage(keyword: string){
    return this.http.get(`${this.baseAPIUrl}/photo?keyword=${keyword}`);
  }

  getArtistsNext(skipNum: number){
    return this.http.get(`${this.baseAPIUrl}?$top=10&$orderby=Name&$skip=${skipNum}`);
  }

  getArtistsPrev(skipNum: number){
    return this.http.get(`${this.baseAPIUrl}?$top=10&$orderby=Name&$skip=${skipNum}`);
  }

  sort(fieldName: string, asc: boolean){
    let url = this.http.get(`${this.baseAPIUrl}?$top=10&$OrderBy=${fieldName} ${ asc ? 'asc' : 'desc' }`)
    if(fieldName == 'Album') {
      url = this.http.get(`${this.baseAPIUrl}?$top=10&$expand=Album($select=Title,AlbumId;$OrderBy=Title ${ asc ? 'asc' : 'desc' })`);
    }
    return url;
  }

  getArtistNames(term: string){
    return this.http.get(`${this.baseAPIUrl}?$top=10&$orderby=Name&$select=Name&$filter=contains(Name, '${term}')`);
  }
}
