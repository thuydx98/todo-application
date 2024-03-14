import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ApiResult, WorkItem } from '../../models';
import { WorkItemService } from '../../services/work-item.service';
import { catchError, map, of } from 'rxjs';

@Component({
  selector: 'app-work-item-list',
  templateUrl: './work-item-list.component.html',
  styleUrl: './work-item-list.component.css',
})
export class WorkItemListComponent implements OnInit {
  @Output() onSelect: EventEmitter<any> = new EventEmitter();

  workItems?: ApiResult<WorkItem[]>;

  constructor(
    private workItemService: WorkItemService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.workItems = undefined;
    this.workItemService.getListWorkItem().subscribe(
      (response) => {
        this.workItems = response;
      },
      (error) => {
        this.workItems = {
          success: false,
          errorCode: 0,
          errorMessage: undefined,
          result: [],
        };
      }
    );
  }

  onSelectWorkItem(workItem: WorkItem) {
    this.onSelect.emit(Object.assign({}, workItem));
  }

  onDelete(workItem: WorkItem) {
    if (confirm(`Are you sure to delete "${workItem.content}" ?`)) {
      this.workItemService.deleteWorkItem(workItem.id).subscribe(
        (res) => {
          this.toastr.success('Detete work item succeeded', 'Work Items');
          if (this.workItems) {
            this.workItems.result = this.workItems?.result.filter(
              (i) => i.id !== workItem.id
            );
          }
        },
        (err) => {
          this.toastr.error('Detete work item failed', 'Work Items');
        }
      );
    }
  }
}
