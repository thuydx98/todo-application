import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { Observable, Subject, map } from 'rxjs';
import { environment } from '../../environments/environment';
import { ApiResult, WorkItem } from '../models';

@Injectable({
  providedIn: 'root',
})
export class WorkItemService implements OnDestroy {
  private readonly worItemApiUrl = `${environment.apiUrl}/api/work-items`;
  private _workItems$: Subject<WorkItem[]> = new Subject<WorkItem[]>();

  public workItems$: Observable<WorkItem[]> = this._workItems$.asObservable();

  constructor(private readonly http: HttpClient) {}

  ngOnDestroy(): void {
    this._workItems$.complete();
  }

  public getListWorkItem(): Observable<ApiResult<WorkItem[]>> {
    return this.http.get<ApiResult<WorkItem[]>>(this.worItemApiUrl);
  }

  public createWorkItem(workItem: WorkItem): Observable<ApiResult<WorkItem>> {
    return this.http.post<ApiResult<WorkItem>>(this.worItemApiUrl, workItem);
  }

  public updateWorkItem(workItem: WorkItem): Observable<ApiResult<WorkItem>> {
    return this.http.put<ApiResult<WorkItem>>(`${this.worItemApiUrl}/${workItem.id}`, workItem);
  }

  public deleteWorkItem(id: string): Observable<ApiResult<WorkItem>> {
    return this.http.delete<ApiResult<WorkItem>>(`${this.worItemApiUrl}/${id}`);
  }
}
