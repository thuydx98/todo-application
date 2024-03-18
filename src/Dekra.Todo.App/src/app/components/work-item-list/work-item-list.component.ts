import { Component, OnInit } from '@angular/core';
import { WorkItem } from '../../models';
import { Store, select } from '@ngrx/store';
import { ApiRequestStatus, WorkItemState } from '../../store/state/work-item.state';
import * as fromWorkItemState from '../../store/selectors/work-item.selectors';
import { Observable } from 'rxjs';
import { deleteWorkItem, getWorkItems, selectWorkItem, updateWorkItem } from '../../store/actions/work-item.actions';

@Component({
  selector: 'app-work-item-list',
  templateUrl: './work-item-list.component.html',
})
export class WorkItemListComponent implements OnInit {
  ApiRequestStatus = ApiRequestStatus;

  workItems$?: Observable<WorkItem[]>;
  getWorkItemStatus$?: Observable<ApiRequestStatus | undefined>;

  constructor(private store: Store<WorkItemState>) {}

  ngOnInit() {
    this.onLoadWorkItems();
    this.workItems$ = this.store.pipe(select(fromWorkItemState.workItems));
    this.getWorkItemStatus$ = this.store.pipe(select(fromWorkItemState.getWorkItemStatus));
  }

  onLoadWorkItems() {
    this.store.dispatch(getWorkItems());
  }

  onSelectWorkItem(workItem: WorkItem) {
    this.store.dispatch(selectWorkItem({ payload: {...workItem }}));
  }

  onDelete(workItem: WorkItem) {
    if (confirm(`Are you sure to delete "${workItem.content}" ?`)) {
      this.store.dispatch(deleteWorkItem({ payload: workItem }));
    }
  }

  onMarkDone(workItem: WorkItem) {
    this.store.dispatch(updateWorkItem({ payload: {...workItem, isCompleted: !workItem.isCompleted} }));
  }
}
