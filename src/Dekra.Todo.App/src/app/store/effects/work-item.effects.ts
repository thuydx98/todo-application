import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { WorkItemService } from '../../services/work-item.service';
import { WorkItemState } from '../state/work-item.state';
import { Store } from '@ngrx/store';
import * as workItemActions from '../actions/work-item.actions';
import { catchError, map, of, switchMap } from 'rxjs';
import { WorkItem } from '../../models';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable()
export class WorkItemEffects {
  constructor(private actions$: Actions, private workItemService: WorkItemService, private store: Store<WorkItemState>) {}

  getWorkItems$ = createEffect(() =>
    this.actions$.pipe(
      ofType(workItemActions.getWorkItems),
      switchMap(() =>
        this.workItemService.getListWorkItem().pipe(
          map((res) => workItemActions.getWorkItemsSuccess({ payload: res.result })),
          catchError((error: HttpErrorResponse) => of(workItemActions.getWorkItemsFail()))
        )
      )
    )
  );

  createWorkItem$ = createEffect(() =>
    this.actions$.pipe(
      ofType(workItemActions.createWorkItem),
      map((action: any) => action.payload),
      switchMap((workItem: WorkItem) =>
        this.workItemService.createWorkItem(workItem).pipe(
          map((res) => workItemActions.createWorkItemSuccess({ payload: res.result })),
          catchError((error: HttpErrorResponse) => of(workItemActions.createWorkItemFail()))
        )
      )
    )
  );

  updateWorkItem$ = createEffect(() =>
    this.actions$.pipe(
      ofType(workItemActions.updateWorkItem),
      map((action: any) => action.payload),
      switchMap((workItem: WorkItem) =>
        this.workItemService.updateWorkItem(workItem).pipe(
          map((res) => workItemActions.updateWorkItemSuccess({ payload: res.result })),
          catchError((error: HttpErrorResponse) => of(workItemActions.updateWorkItemFail()))
        )
      )
    )
  );

  deleteWorkItem$ = createEffect(() =>
    this.actions$.pipe(
      ofType(workItemActions.deleteWorkItem),
      map((action: any) => action.payload),
      switchMap((workItem: WorkItem) =>
        this.workItemService.deleteWorkItem(workItem.id!).pipe(
          map((res) => workItemActions.deleteWorkItemSuccess({ payload: res.result })),
          catchError((error: HttpErrorResponse) => of(workItemActions.deleteWorkItemFail()))
        )
      )
    )
  );
}
