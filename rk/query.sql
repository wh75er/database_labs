select * from rk3.students s
JOIN rk3.teachers t ON s.Spec = t.Spec
where s.teacherId is NULL and t.People < ALL(
    select count(*) as amount from rk3.students
    group by teacherId
)

select count(*), MAX(t.People) from rk3.teachers t
JOIN rk3.students s ON s.teacherId = t.id
GROUP BY s.teacherId