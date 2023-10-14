import { Component } from '@angular/core';
import { DialogService } from 'core/dialog.service';
import { CreateResearchMethodology, ResearchMethodology, QueryResearchMethodology, UpdateResearchMethodology } from '../../../api/api.types';
import { ResearchMethodologyApi } from '../../../api/research-methodology.api';
import { StaticDataComponent } from '../shared/static-data.component';

@Component({
  selector: 'research-methodology',
  templateUrl: '../shared/static-data.component.html'
})
export class ResearchMethodologyComponent extends StaticDataComponent<ResearchMethodology, QueryResearchMethodology, CreateResearchMethodology, UpdateResearchMethodology> {
  constructor(
    dataSource: ResearchMethodologyApi,
  ) {
    super(dataSource);
    this.typeName = "Research Methodology";
  }
}
