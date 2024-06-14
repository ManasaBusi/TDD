﻿using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;

namespace DeskBooker.Core.Processor
{
    public class DeskBookingRequestProcessor : IDeskBookingRequestProcessor
    {
        private readonly IDeskBookingRepository _deskBookingRepository;
        private readonly IDeskRepository _deskRepository;

        public DeskBookingRequestProcessor(IDeskBookingRepository deskBookingRepository, IDeskRepository deskRepository)
        {
            _deskBookingRepository = deskBookingRepository;
            _deskRepository = deskRepository;
        }

        public DeskBookingResult BookDesk(DeskBookingRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = Create<DeskBookingResult>(request);

            var avaialbleDesks = _deskRepository.GetAvailableDesks(request.Date);

            if (avaialbleDesks.FirstOrDefault() is Desk availableDesk)
            {
                //var availableDesk = avaialbleDesks.First();

                var deskBooking = Create<DeskBooking>(request);

                deskBooking.DeskId = availableDesk.Id;

                _deskBookingRepository.Save(deskBooking);

                result.Code = DeskBookingResultCode.Success;
                result.DeskBookingId = deskBooking.Id;
            }

            else
            {
                result.Code = DeskBookingResultCode.NoDeskAvailable;
            }

            return result;
        }
        private static T Create<T>(DeskBookingRequest request) where T : DeskBoookingBase, new()
        {
            return new T
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Date = request.Date
            };
        }

    }

}
