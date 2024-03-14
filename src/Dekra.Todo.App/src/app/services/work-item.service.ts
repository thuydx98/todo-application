import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from '../../environments/environment';
import { ApiResult, WorkItem } from '../models';

@Injectable({
  providedIn: 'root',
})
export class WorkItemService {
  private readonly worItemApiUrl = `${environment.apiUrl}/api/work-items`;

  constructor(private http: HttpClient) {}

  public getListWorkItem(): Observable<ApiResult<WorkItem[]>> {
    return this.http
      .get<ApiResult<WorkItem[]>>(this.worItemApiUrl)
      .pipe(map((response) => response));
  }

  public createWorkItem(workItem: WorkItem): Observable<ApiResult<WorkItem>> {
    return this.http.post<ApiResult<WorkItem>>(this.worItemApiUrl, workItem);
  }

  public updateWorkItem(workItem: WorkItem): Observable<ApiResult<WorkItem>> {
    return this.http.put<ApiResult<WorkItem>>(
      `${this.worItemApiUrl}/${workItem.id}`,
      workItem
    );
  }

  public deleteWorkItem(id: string): Observable<ApiResult<WorkItem>> {
    return this.http.delete<ApiResult<WorkItem>>(`${this.worItemApiUrl}/${id}`);
  }
}
