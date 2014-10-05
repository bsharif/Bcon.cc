# coding: utf8
import hashlib


# generate hash from users email and use it for hash key, insert it into profile.hash_key
def make_hash(raw):
    h = hashlib.new('ripemd160')
    h.update(raw)
    return h.hexdigest()


db.define_table('profile',
                Field('hash_key','string',readable=False,writable=False),
                Field('first_name','string',requires=IS_NOT_EMPTY()),
                Field('last_name','string',requires=IS_NOT_EMPTY()),
                Field('date_of_birth','date',requires=IS_DATE()),
                Field('profile_picture','upload'),
                Field('emergency_contact_name','string',requires=IS_NOT_EMPTY()),
                Field('emergency_contact_number','string',requires=IS_NOT_EMPTY()),
                Field('nhs_number','string',requires=IS_NOT_EMPTY()),
                Field('allergies','text',requires=IS_NOT_EMPTY()),
                Field('medication','text',requires=IS_NOT_EMPTY()),
                Field('medical_conditions','text',requires=IS_NOT_EMPTY()),
                auth.signature)
