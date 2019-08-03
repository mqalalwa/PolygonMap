import { Shape } from './Shape.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Polygon } from './polygon.model';
import { environment } from 'src/environments/environment';

@Injectable()
export class ApiService {
  api = 'PolygonMap';
  constructor(private http: HttpClient) { }
  getShapes() {
    return this.http.get<Shape[]>(`${environment.api}${this.api}/GetAllShapeAsync`);
  }
  getPolygonTodraw(name,shapeid,lat,lng) {
    return this.http.get<Shape>(`${environment.api}${this.api}/CalPointsWithNewCenterAsync/${shapeid}/${name}/${lng}/${lat}`);
  }
  UpdatePolygon(polygonid:number,polygon: Polygon) {
    return this.http.put(`${environment.api}${this.api}/UpdatePolygonByIdAsync/${polygonid}`, polygon)
  }
  AddPolygon(polygon: Polygon){
    return this.http.post(`${environment.api}${this.api}`, polygon)

  }
  deletePolygogn(polygonId) {
    return this.http.delete(`${environment.api}${this.api}/DeletePolygonByIdAsync/${polygonId}`)
  }
}
