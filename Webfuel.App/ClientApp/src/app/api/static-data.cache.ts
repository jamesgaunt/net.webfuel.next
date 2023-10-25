import { Injectable } from '@angular/core';
import { IDataSource, IDataSourceWithGet } from 'shared/common/data-source';
import { StaticDataService } from '../core/static-data.service';
import { ApplicationStage, FundingBody, FundingCallType, FundingStream, Gender, HowDidYouFindUs, IsCTUTeamContribution, IsFellowship, IsInternationalMultiSiteStudy, IsLeadApplicantNHS, IsPPIEAndEDIContribution, IsQuantativeTeamContribution, IsResubmission, IsTeamMembersConsulted, ProjectStatus, ResearchMethodology, Site, SubmissionOutcome, SubmissionStage, SupportProvided, SupportRequestStatus, Title, UserDiscipline, WorkActivity } from './api.types';

@Injectable()
export class StaticDataCache {
    
    constructor(private staticDataService: StaticDataService) {
    }
    
    applicationStage: IDataSourceWithGet<ApplicationStage> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.applicationStage),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.applicationStage),
    };
    
    fundingBody: IDataSourceWithGet<FundingBody> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.fundingBody),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.fundingBody),
    };
    
    fundingCallType: IDataSourceWithGet<FundingCallType> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.fundingCallType),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.fundingCallType),
    };
    
    fundingStream: IDataSourceWithGet<FundingStream> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.fundingStream),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.fundingStream),
    };
    
    gender: IDataSourceWithGet<Gender> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.gender),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.gender),
    };
    
    howDidYouFindUs: IDataSourceWithGet<HowDidYouFindUs> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.howDidYouFindUs),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.howDidYouFindUs),
    };
    
    isCTUTeamContribution: IDataSourceWithGet<IsCTUTeamContribution> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isCTUTeamContribution),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isCTUTeamContribution),
    };
    
    isFellowship: IDataSourceWithGet<IsFellowship> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isFellowship),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isFellowship),
    };
    
    isInternationalMultiSiteStudy: IDataSourceWithGet<IsInternationalMultiSiteStudy> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isInternationalMultiSiteStudy),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isInternationalMultiSiteStudy),
    };
    
    isLeadApplicantNHS: IDataSourceWithGet<IsLeadApplicantNHS> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isLeadApplicantNHS),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isLeadApplicantNHS),
    };
    
    isPPIEAndEDIContribution: IDataSourceWithGet<IsPPIEAndEDIContribution> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isPPIEAndEDIContribution),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isPPIEAndEDIContribution),
    };
    
    isQuantativeTeamContribution: IDataSourceWithGet<IsQuantativeTeamContribution> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isQuantativeTeamContribution),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isQuantativeTeamContribution),
    };
    
    isResubmission: IDataSourceWithGet<IsResubmission> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isResubmission),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isResubmission),
    };
    
    isTeamMembersConsulted: IDataSourceWithGet<IsTeamMembersConsulted> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isTeamMembersConsulted),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isTeamMembersConsulted),
    };
    
    projectStatus: IDataSourceWithGet<ProjectStatus> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.projectStatus),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.projectStatus),
    };
    
    researchMethodology: IDataSourceWithGet<ResearchMethodology> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.researchMethodology),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.researchMethodology),
    };
    
    site: IDataSourceWithGet<Site> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.site),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.site),
    };
    
    submissionOutcome: IDataSourceWithGet<SubmissionOutcome> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.submissionOutcome),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.submissionOutcome),
    };
    
    submissionStage: IDataSourceWithGet<SubmissionStage> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.submissionStage),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.submissionStage),
    };
    
    supportProvided: IDataSourceWithGet<SupportProvided> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.supportProvided),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.supportProvided),
    };
    
    supportRequestStatus: IDataSourceWithGet<SupportRequestStatus> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.supportRequestStatus),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.supportRequestStatus),
    };
    
    title: IDataSourceWithGet<Title> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.title),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.title),
    };
    
    userDiscipline: IDataSourceWithGet<UserDiscipline> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.userDiscipline),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.userDiscipline),
    };
    
    workActivity: IDataSourceWithGet<WorkActivity> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.workActivity),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.workActivity),
    };
}

