// Copyright (C) 2023-2024 Emad Abbasnezhad (real_emad_abbasnezhad@proton.me)
// This file is a part of EmadKala, Licenced under http://www.gnu.org/licenses/

namespace EmadKalaWeb.Services;

public interface IEmadKalaSmsService
{
}

// [Quick Start] hare you must implant the way you send sms to user.
public class EmadKalaSmsService : IEmadKalaSmsService
{
    // this welcome message also have a confirmation code for this phone number
    public async Task SendWelcomeAsync(string phoneNumber, string confirmationCode)
    {
        await new TaskFactory().StartNew(() => { });
    }
}