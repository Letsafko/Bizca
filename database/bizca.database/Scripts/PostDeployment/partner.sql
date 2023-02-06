merge into [ref].[partner] as target
    using
    (
        values 
            (1, 'bizca', 'bizca', 24 * 60, 10, 0, 4)
    ) as source 
    (
        partnerId, 
        partnerCode, 
        description,
        confirmation_code_delay_in_minutes,
        confirmation_code_length,
        mandatory_address_field_mask,
        mandatory_user_profile_field_mask
    ) on target.partnerId = source.partnerId
    when matched then
    update
        set partnerCode = source.partnerCode,
            description = source.description,
            confirmation_code_length  = source.confirmation_code_length,
            mandatory_address_field_mask  = source.mandatory_address_field_mask ,
            mandatory_user_profile_field_mask  = source.mandatory_user_profile_field_mask,
            confirmation_code_delay_in_minutes = source.confirmation_code_delay_in_minutes,
            lastUpdate = getdate()
    when not matched by target then
    insert
    (
        partnerId,
        partnerCode,
        description,
        confirmation_code_delay_in_minutes,
        confirmation_code_length,
        mandatory_address_field_mask,
        mandatory_user_profile_field_mask
    )
    values
    (
        source.partnerId,
        source.partnerCode,
        source.description,
        source.confirmation_code_delay_in_minutes,
        source.confirmation_code_length,
        source.mandatory_address_field_mask,
        source.mandatory_user_profile_field_mask
    );
go