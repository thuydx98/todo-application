import { Component, OnInit } from '@angular/core';
import { WorkItem } from '../../models';
import { Observable } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { ApiRequestStatus, WorkItemState } from '../../store/state/work-item.state';
import * as fromWorkItemState from '../../store/selectors/work-item.selectors';
import { createWorkItem, setSelectWorkItem, updateWorkItem } from '../../store/actions/work-item.actions';

@Component({
  selector: 'app-create-update-work-item',
  templateUrl: './create-update-work-item.component.html',
})
export class CreateUpdateWorkItemComponent implements OnInit {
  ApiRequestStatus = ApiRequestStatus;

  workItem$?: Observable<WorkItem>;
  createWorkItemStatus$?: Observable<ApiRequestStatus | undefined>;
  updateWorkItemStatus$?: Observable<ApiRequestStatus | undefined>;
  createUpdateErrorMessage$?: Observable<string | undefined>;

  submittedEmptyContent = false;

  constructor(private store: Store<WorkItemState>) {}

  ngOnInit(): void {
    this.workItem$ = this.store.pipe(select(fromWorkItemState.selectedWorkItem));
    this.createWorkItemStatus$ = this.store.pipe(select(fromWorkItemState.createWorkItemStatus));
    this.updateWorkItemStatus$ = this.store.pipe(select(fromWorkItemState.updateWorkItemStatus));
    this.createUpdateErrorMessage$ = this.store.pipe(select(fromWorkItemState.createUpdateErrorMessage));
  }

  onCancel() {
    this.store.dispatch(setSelectWorkItem({ payload: new WorkItem() }));
  }

  onChangeWorkItem(event: Event, workItem: WorkItem) {
    this.store.dispatch(setSelectWorkItem({ payload: { ...workItem, content: (event?.target as HTMLTextAreaElement).value } }));
  }

  onCreateUpdateWorkItem(workItem: WorkItem) {
    this.submittedEmptyContent = !workItem.content;
    if (this.submittedEmptyContent) {
      return;
    }
    const action = workItem.id ? updateWorkItem({ payload: { ...workItem } }) : createWorkItem({ payload: { ...workItem } });
    this.store.dispatch(action);
  }
}
