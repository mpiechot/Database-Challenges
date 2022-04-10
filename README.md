# Database-Challenges

This repository contains sql-challenges to stay fit with sql-stuff :)

# Setup

Database: https://www.imdb.com/interfaces/

**cmd:**
```
docker exec -it postgres psql -U gast -d <database>
```

**FileNames:**
```
title.principals -> principals.tsv
title.akas -> titles.tsv
title.basics -> basics.tsv
title.crew -> crew.tsv
title.episode -> episode.tsv
title.ratings -> ratings.tsv
name.basics -> actors.tsv
```


```sql
DROP TABLE IF EXISTS Actor;
CREATE TABLE Actor(nconst text PRIMARY KEY, primaryName text, birthyear integer, deathyear integer, primaryprofession text[], knownfortitles text[]);
COPY actor (nconst, primaryname, birthyear, deathyear, primaryprofession, knownfortitles) FROM '/var/lib/postgresql/data/actors.tsv' DELIMITER E'\t' NULL as '\N' CSV HEADER;

DROP TABLE IF EXISTS Basic;
CREATE TABLE Basic(tconst text PRIMARY KEY, titleType text, primaryTitle text, originalTitle text, isAdult integer, startYear integer, endYear integer, runtimeMinutes integer, genres text[]);
COPY basic (tconst, titletype, primarytitle, originaltitle, isadult, startyear, endyear, runtimeminutes, genres) FROM '/var/lib/postgresql/data/basics.tsv' DELIMITER E'\t' NULL as '\N' CSV HEADER;

DROP TABLE IF EXISTS Crew;
CREATE TABLE Crew(tconst text PRIMARY KEY, directors text[], writers text[]);
COPY crew (tconst, directors, writers) FROM '/var/lib/postgresql/data/crew.tsv' DELIMITER E'\t' NULL as '\N' CSV HEADER;

DROP TABLE IF EXISTS Episode;
CREATE TABLE Episode(tconst text PRIMARY KEY, parentTconst text, seasonNumber integer, episodeNumber integer);
COPY episode (tconst, parentTconst, seasonNumber, episodeNumber) FROM '/var/lib/postgresql/data/episode.tsv' DELIMITER E'\t' NULL as '\N' CSV HEADER;

DROP TABLE IF EXISTS Principal;
CREATE TABLE Principal(tconst text, ordering integer, nconst text, category text, job text, characters text);
COPY principal (tconst, ordering, nconst, category, job, characters) FROM '/var/lib/postgresql/data/principals.tsv' DELIMITER E'\t' NULL as '\N' CSV HEADER;

DROP TABLE IF EXISTS Rating;
CREATE TABLE Rating(tconst text PRIMARY KEY, averagerating real, numvotes integer);
COPY rating (tconst, averagerating, numvotes) FROM '/var/lib/postgresql/data/ratings.tsv' DELIMITER E'\t' NULL as '\N' CSV HEADER;

DROP TABLE IF EXISTS Title;
CREATE TABLE Title(tconst text, ordering integer, title text, region text, language text, types text, attributes text, isOriginalTitle integer);
COPY title (tconst, ordering, title, region, language, types, attributes, isOriginalTitle) FROM '/var/lib/postgresql/data/titles.tsv' DELIMITER E'\t' NULL as '\N' CSV HEADER;
```

## Challenge 1: (22.02.2022 - 13.03.2022)

Get the name of the actors with the most films before the year 2000 (endYear <= 2000)

**Anzahl Filme pro Schauspieler mit Rang**
```sql
SELECT t.primaryname, t.rank
FROM (
  SELECT actor.primaryname, DENSE_RANK() OVER(ORDER BY COUNT(*) DESC) AS rank
  FROM title, basic, principal, actor
  WHERE title.tconst = basic.tconst
  AND principal.tconst = title.tconst
  AND principal.nconst = actor.nconst
  AND title.types = 'original'
  AND basic.titletype = 'movie'
  AND basic.isadult = 0
  AND (principal.category = 'actor' or principal.category = 'actress')
  AND ('actor' = ANY(actor.primaryprofession) or
       'actress' = ANY(actor.primaryprofession))
  GROUP BY actor.nconst, actor.primaryname
  ORDER BY rank) AS t
WHERE t.rank = 1;
```

```sql
SELECT t.primaryname, t.rank
FROM (
  SELECT actor.primaryname, DENSE_RANK() OVER(ORDER BY COUNT(*) DESC) AS rank
  FROM title, basic, principal, actor
  WHERE title.tconst = basic.tconst
  AND principal.tconst = title.tconst
  AND principal.nconst = actor.nconst
  AND title.types = 'original'
  AND basic.titletype = 'movie'
  AND basic.isadult = 0
  AND (principal.category = 'actor' or principal.category = 'actress')
  AND ('actor' = ANY(actor.primaryprofession) or
       'actress' = ANY(actor.primaryprofession))
  GROUP BY actor.nconst, actor.primaryname
  ORDER BY rank) AS t
WHERE t.primaryName LIKE 'Nicolas Cage';
```

## Challenge 2: (10.04.2022 - 24.04.2022)

Get a list of genres with the highest rated movie (rating/votes) per genre.
