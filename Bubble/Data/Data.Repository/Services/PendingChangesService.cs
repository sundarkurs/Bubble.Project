using Data.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Data.ClientModels.CustomerService;
using Data.Models.OracleDb;
using AutoMapper;

namespace Data.Repository.Services
{
    public class PendingChangesService : IPendingChangesService
    {
        bool IPendingChangesService.Approve(int paxId)
        {
            throw new NotImplementedException();
        }

        PaxPendingChange IPendingChangesService.GetPaxPendingChanges(int paxId)
        {
            using (var context = new BluecowEntities())
            {
                var paxChanges = (
                    from BP in context.CTBookings_Passenger
                    join PP in context.CTPassengers_Passenger
                    on BP.PAX_ID equals PP.PAX_ID
                    where BP.PAX_ID == paxId
                    select new PaxPendingChange
                    {
                        MasterData = new BookingPassenger()
                        {
                            PaxId = BP.PAX_ID,
                            FirstName = BP.FIRST_NAME,
                            FamilyName = BP.FAMILY_NAME,

                            MiddleName = BP.MIDDLE_NAME,
                            DateOfBirth = BP.DOB,
                            Gender = BP.GENDER,
                            PassportNationality = BP.PSPT_NATIONALITY,
                            PassportBirthPlace = BP.PSPT_BIRTH_PLACE,
                            PassportNumber = BP.PSPT_NO,
                            PassportIssueDate = BP.PSPT_ISS_DATE,
                            PassportExpiryDate = BP.PSPT_EXP_DATE
                        },
                        StagedData = new ModifiedPassenger()
                        {
                            PaxId = BP.PAX_ID,

                            MiddleName = PP.MIDDLE_NAME,
                            Gender = PP.GENDER,
                            PassportNationality = PP.PSPT_NATIONALITY,
                            PassportBirthPlace = PP.PSPT_BIRTH_PLACE,
                            PassportNumber = PP.PSPT_NO,
                            PassportIssueDate = PP.PSPT_ISS_DATE,
                            PassportExpiryDate = PP.PSPT_EXP_DATE
                        }
                    }).FirstOrDefault();

                return paxChanges;
            }
        }

        List<PendingChange> IPendingChangesService.GetPendingChanges()
        {
            using (var context = new BluecowEntities())
            {
                var changes = context.CTApplications_PendingChanges.ToList();
                return Mapper.Map<List<CTApplications_PendingChanges>, List<PendingChange>>(changes);
            }
        }

        bool IPendingChangesService.Reject(int paxId)
        {
            throw new NotImplementedException();
        }
    }
}
