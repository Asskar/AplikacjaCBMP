﻿using AppCBMP.DAL.Repositories;

namespace AppCBMP.DAL.Persistence
{
    public interface IUnitOfWork
    {
        CompanyRepository Company { get; set; }
        FirstNameRepository FirstName { get; set; }
        PersonRepository Person { get; set; }
        PositionRepository Position { get; set; }
        PsychologicalServiceRepository PsychologicalService { get; set; }
        PsychologicalServiceTypesRepository PsychologicalServiceTypes { get; set; }
        PsychologistRepository Psychologist { get; set; }
        ReferralRepository Referral { get; set; }

        void Complete();
    }
}