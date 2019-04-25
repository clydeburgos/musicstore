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

  getArtistsNext(skipNum: number){
    return this.http.get(`${this.baseAPIUrl}?$top=10&$orderby=Name&$skip=${skipNum}`);
  }

  getArtistsPrev(skipNum: number){
    return this.http.get(`${this.baseAPIUrl}?$top=10&$orderby=Name&$skip=${skipNum}`);
  }

  getArtistNames(term: string){
    return this.http.get(`${this.baseAPIUrl}?$top=10&$orderby=Name&$select=Name&$filter=contains(Name, '${term}')`);
  }
}
