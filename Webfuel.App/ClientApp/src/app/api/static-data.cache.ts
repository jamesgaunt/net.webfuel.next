import { Injectable } from '@angular/core';
import { IDataSource } from 'shared/common/data-source';
import { StaticDataService } from '../core/static-data.service';
import { FundingBody, FundingStream, Gender, ResearchMethodology, Title } from './api.types';

@Injectable()
export class StaticDataCache {
    
    constructor(private staticDataService: StaticDataService) {
    }
    
    fundingBody: IDataSource<FundingBody> = { query: (query) => this.staticDataService.load(query, s => s.fundingBody) };
    
    fundingStream: IDataSource<FundingStream> = { query: (query) => this.staticDataService.load(query, s => s.fundingStream) };
    
    gender: IDataSource<Gender> = { query: (query) => this.staticDataService.load(query, s => s.gender) };
    
    researchMethodology: IDataSource<ResearchMethodology> = { query: (query) => this.staticDataService.load(query, s => s.researchMethodology) };
    
    title: IDataSource<Title> = { query: (query) => this.staticDataService.load(query, s => s.title) };
}

