import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LOBSection } from '../models/third-party/CNAE/lobSection';
import { AddressBR } from '../models/third-party/CEP/addressBR';

@Injectable({
  providedIn: 'root'
})
export class ThirdPartyService {

  constructor(
    private http: HttpClient
  ) { }

  private apiCNAE: string = "https://servicodados.ibge.gov.br/api/v2/cnae/";
  private apiVIACEP: string = "https://viacep.com.br/ws/";

  public getLOBGroups(): Observable<LOBSection[]>{
    return this.http.get<LOBSection[]>(this.apiCNAE + "secoes");
  }

  public getAddressWithPostalCodeBR(postalCode: string): Observable<AddressBR>{
    return this.http.get<AddressBR>(this.apiVIACEP + postalCode + "/json");
  }
}
