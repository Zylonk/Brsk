PGDMP     (                    {            todolist    15.5 (Debian 15.5-0+deb12u1)    15.1                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    16384    todolist    DATABASE     t   CREATE DATABASE todolist WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.UTF-8';
    DROP DATABASE todolist;
                postgres    false                       0    0    DATABASE todolist    ACL     (   GRANT ALL ON DATABASE todolist TO vbox;
                   postgres    false    3357                        2615    16390    Todo    SCHEMA        CREATE SCHEMA "Todo";
    DROP SCHEMA "Todo";
                vbox    false            �            1259    16398    Tasks    TABLE     �   CREATE TABLE "Todo"."Tasks" (
    "Id" text NOT NULL,
    "UserId" text NOT NULL,
    "Description" text NOT NULL,
    "Timeframe" timestamp without time zone NOT NULL,
    "Priority" text NOT NULL,
    "Done" boolean NOT NULL
);
    DROP TABLE "Todo"."Tasks";
       Todo         heap    vbox    false    6            �            1259    16391    User    TABLE     w   CREATE TABLE "Todo"."User" (
    "Id" text NOT NULL,
    "Username" text NOT NULL,
    "PasswordHash" text NOT NULL
);
    DROP TABLE "Todo"."User";
       Todo         heap    vbox    false    6                      0    16398    Tasks 
   TABLE DATA           a   COPY "Todo"."Tasks" ("Id", "UserId", "Description", "Timeframe", "Priority", "Done") FROM stdin;
    Todo          vbox    false    216   {                 0    16391    User 
   TABLE DATA           B   COPY "Todo"."User" ("Id", "Username", "PasswordHash") FROM stdin;
    Todo          vbox    false    215   �       �           2606    16404    Tasks Tasks_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY "Todo"."Tasks"
    ADD CONSTRAINT "Tasks_pkey" PRIMARY KEY ("Id");
 >   ALTER TABLE ONLY "Todo"."Tasks" DROP CONSTRAINT "Tasks_pkey";
       Todo            vbox    false    216            �           2606    16397    User User_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY "Todo"."User"
    ADD CONSTRAINT "User_pkey" PRIMARY KEY ("Id");
 <   ALTER TABLE ONLY "Todo"."User" DROP CONSTRAINT "User_pkey";
       Todo            vbox    false    215            �           2606    16405    Tasks Fk_task_user    FK CONSTRAINT     y   ALTER TABLE ONLY "Todo"."Tasks"
    ADD CONSTRAINT "Fk_task_user" FOREIGN KEY ("UserId") REFERENCES "Todo"."User"("Id");
 @   ALTER TABLE ONLY "Todo"."Tasks" DROP CONSTRAINT "Fk_task_user";
       Todo          vbox    false    215    3204    216               7  x���9ND1Dc�Sp�Fnw/dH�d,BH$^Y5�@"���; � ��#�@�F�;*ի���䓠�2����/��l\ߧhK29K�t����  -H���D��������"J��Z1-VzJ|7L����-7Nc\�G�P �h�%����
�����D�����v�(F(�k)�O�����![�'j���`�'�1h"#�WwwV����76���jF�b{��=$�{lu,1H��Z�Qχn�I}l�����ѕPbt9�u,����:�98������$8�Z�\��o��/p�㓳�Ӌ0� -v]��v�9           x�M�ˎ�F���9f[��v���7��6���ʦ���n�M)�y��A6��"%R^�(�&�N�쒥�?���N����S㙲��3����4FJ��<�@��� ¥�1�6�>��N��'�B�>_���V"D���[���`�`�҅ђ	�vX��g��j��8N-$4�ԓ@S���\PǇ�2�zD
��]��"I'*�cZ��^=�7��}��V�#�m��md�
y�?��ִ�@
́4�O(PD0@B!��S��?(�_���p�h�h��~?�z���I����<��Q�xV~�Οq)Ɯ`������
J�1\@!1B�^t�=@a�^����lہ��L�is7������������,<{��ƭDۖ$�`��4A��yŀH��9��t�����ۃEP�a2n��c(.S{��&��bܾ���d�w�e�y+�~c$w�H[�AP
� ��D�A�z�a�$5��V���3���!@AѾ�Y��,�Y���s�έy������[��+7b�Yt���mZ�Hv*��<�j�r!�Í����T���c0��$��IoT4���[�AC�����&:�����e���a��"��k3����wY#�z��0gp���@��pm�Ct��h��(l��O����C�f��J4��z�U�{/����[j���� ��0rYQ���%>BTC�1�N1��M��xf�[8d:�M�h����N�v�,`�E�NsaO�����m�\	�\�]S� ��
C`� @A���q� ƫd��$�ب��V[R�ZOJ��컫�m};^�'�O��?���$x�SSdq�s'��CQ�u`�:;���V��8�R�J�sw�>�cW���NMOU��LU��?�V�
'��ړ�5	�CG��e����l��e~wi7����N��{��~_�G��0��k���f;�ߓ����*j��bt�ˁDrD�=�u�v�z������ۗ�Ϸ/�_��T�]�]��~SY���8��Ԡ�������}5��{��$�OZ���DȄ&]{�){&r���~�-���t��     